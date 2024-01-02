pipeline {
    agent any

    stages {
        stage('Checkout') {
            steps {
                checkout scmGit(branches: [[name: '*/master']], extensions: [], userRemoteConfigs: [[url: 'https://github.com/sergioferreira0122/library-web-api-dotnet']])
            }
        }
        
        stage('Build image') {
            steps {
                script {
                    bat 'docker build -t pyteeee/libraryapi:latest .'
                }
            }
        }
        
        stage('Run Tests') {
            steps {
                script {
                    bat 'dotnet test Library.Tests/Library.Tests.csproj'
                }
            }
        }
        
        stage('Deploy') {
            steps {
                script {
                    bat 'docker push pyteeee/libraryapi:latest'
                }
            }
        }
    }
}
