pipeline {
    agent any

    environment {
        PROJECT_NAME = 'IOT_Shopping'
        DOCKER_IMAGE = 'iot-shopping:latest'
        CONTAINER_NAME = 'iot-shopping-container'
        APP_URL = 'http://localhost:8085'
    }

    stages {

        stage('Checkout') {
            steps {
                echo 'Recuperation du code depuis Git...'
                checkout scm
            }
        }

        stage('Build') {
            steps {
                echo 'Build du projet .NET...'
                bat 'dotnet restore'
                bat 'dotnet build --configuration Release --no-restore'
            }
        }

    }
}
