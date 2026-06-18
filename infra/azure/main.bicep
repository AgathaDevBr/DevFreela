@description('Azure region for the resources.')
param location string = resourceGroup().location

@description('Application name used for Azure resource names.')
param appName string = 'devfreela'

@description('Container image to run in Azure Container Apps.')
param containerImage string

@description('SQL Server administrator login.')
param sqlAdminLogin string = 'devfreelaadmin'

@secure()
@description('SQL Server administrator password.')
param sqlAdminPassword string

@secure()
@description('JWT signing secret used by the API.')
param jwtSecret string

@description('RabbitMQ host name used by the API.')
param rabbitMqHost string

@description('RabbitMQ user name.')
param rabbitMqUserName string = 'guest'

@secure()
@description('RabbitMQ password.')
param rabbitMqPassword string

var sqlServerName = toLower('${appName}-${uniqueString(resourceGroup().id)}')
var databaseName = '${appName}-db'
var logAnalyticsName = '${appName}-logs'
var environmentName = '${appName}-env'

resource logAnalytics 'Microsoft.OperationalInsights/workspaces@2023-09-01' = {
  name: logAnalyticsName
  location: location
  properties: {
    sku: {
      name: 'PerGB2018'
    }
    retentionInDays: 30
  }
}

resource containerEnvironment 'Microsoft.App/managedEnvironments@2024-03-01' = {
  name: environmentName
  location: location
  properties: {
    appLogsConfiguration: {
      destination: 'log-analytics'
      logAnalyticsConfiguration: {
        customerId: logAnalytics.properties.customerId
        sharedKey: logAnalytics.listKeys().primarySharedKey
      }
    }
  }
}

resource sqlServer 'Microsoft.Sql/servers@2023-08-01-preview' = {
  name: sqlServerName
  location: location
  properties: {
    administratorLogin: sqlAdminLogin
    administratorLoginPassword: sqlAdminPassword
    version: '12.0'
  }
}

resource database 'Microsoft.Sql/servers/databases@2023-08-01-preview' = {
  name: databaseName
  parent: sqlServer
  location: location
  sku: {
    name: 'Basic'
    tier: 'Basic'
  }
}

resource containerApp 'Microsoft.App/containerApps@2024-03-01' = {
  name: appName
  location: location
  properties: {
    managedEnvironmentId: containerEnvironment.id
    configuration: {
      ingress: {
        external: true
        targetPort: 8080
        transport: 'auto'
      }
      secrets: [
        {
          name: 'jwt-secret'
          value: jwtSecret
        }
        {
          name: 'sql-connection-string'
          value: 'Server=tcp:${sqlServer.properties.fullyQualifiedDomainName},1433;Initial Catalog=${database.name};Persist Security Info=False;User ID=${sqlAdminLogin};Password=${sqlAdminPassword};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;'
        }
        {
          name: 'rabbitmq-password'
          value: rabbitMqPassword
        }
      ]
    }
    template: {
      containers: [
        {
          name: 'api'
          image: containerImage
          env: [
            {
              name: 'ASPNETCORE_ENVIRONMENT'
              value: 'Production'
            }
            {
              name: 'ConnectionStrings__DevFreelaCs'
              secretRef: 'sql-connection-string'
            }
            {
              name: 'Jwt__Issuer'
              value: 'DevFreela'
            }
            {
              name: 'Jwt__Audience'
              value: 'DevFreela'
            }
            {
              name: 'Jwt__SecretKey'
              secretRef: 'jwt-secret'
            }
            {
              name: 'RabbitMQ__HostName'
              value: rabbitMqHost
            }
            {
              name: 'RabbitMQ__UserName'
              value: rabbitMqUserName
            }
            {
              name: 'RabbitMQ__Password'
              secretRef: 'rabbitmq-password'
            }
          ]
          resources: {
            cpu: json('0.5')
            memory: '1Gi'
          }
        }
      ]
      scale: {
        minReplicas: 1
        maxReplicas: 3
      }
    }
  }
}

output apiUrl string = 'https://${containerApp.properties.configuration.ingress.fqdn}'
