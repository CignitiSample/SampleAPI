pipeline {
    agent any

    stages {
        stage('Build') {
            steps {
                bat "\"${tool 'MsBuild'}\" MCS.Test.Automation.Nunit.csproj /p:Configuration=Release /p:Platform=\"Any CPU\" /p:ProductVersion=1.0.0.${env.BUILD_NUMBER}"
            }
        }
      
    }
} 
