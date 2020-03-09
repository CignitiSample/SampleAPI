pipeline {
    agent any

    stages {
        stage('Restore') {
            steps {
                bat slnName = "${"C:\\Program Files (x86)\\Jenkins\\workspace\\Pipeline_MSBuild\\"}${"TestFramework.sln"}.sln" 
                bat nuget = "\"${WORKSPACE}\\tools\\Nuget\\nuget.exe\""
                bat("${nuget} restore \"${slnName}\"")
            }
        }
        stage('Build') {
            steps {
                bat "\"${tool 'MsBuild'}\" TestFramework.sln /p:Configuration=Release /p:Platform=\"Any CPU\" /p:ProductVersion=1.0.0.${env.BUILD_NUMBER}"
            }
        }
      
    }
      post {
          failure {
                 mail to: 'e005249@cigniti.com',
                 subject: "Pipeline has failed: ${currentBuild.fullDisplayName}",
                 body: "Error in ${env.BUILD_URL}"
                  }
             }
} 
