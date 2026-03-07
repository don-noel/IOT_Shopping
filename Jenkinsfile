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
                echo 'Récupération du code depuis Git...'
                checkout scm
            }
        }

    }
}