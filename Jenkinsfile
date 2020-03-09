pipeline {
    agent any

    stages {
        stage('Build') {
            steps {
                bat "\"${tool 'MsBuild'}\" TestFramework.sln /p:Configuration=Release /p:Platform=\"Any CPU\" /p:ProductVersion=1.0.0.${env.BUILD_NUMBER}"
            }
        }
      
    }
} 
