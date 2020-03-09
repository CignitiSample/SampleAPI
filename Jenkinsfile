pipeline {
    agent any

    stages {
        stage('Restore') {
            steps {
              bat  C:\Program Files (x86)\Nuget\nuget.exe restore C:\Program Files (x86)\Jenkins\workspace\Pipeline_MSBuild\TestFramework.sln
            }
        }
        stage('Build') {
            steps {
                bat "\"${tool 'MsBuild'}\" TestFramework.sln /p:Configuration=Release /p:Platform=\"Any CPU\" /p:ProductVersion=1.0.0.${env.BUILD_NUMBER}"
            }
        }
    }
} 
