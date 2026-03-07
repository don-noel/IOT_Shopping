pipeline {
    agent any

    environment {
        PROJECT_NAME = 'IOT_Shopping'
        DOCKER_IMAGE = 'iot-shopping:latest'
        CONTAINER_NAME = 'iot-shopping-container'
        APP_URL = 'http://localhost:8085'
        SONAR_PROJECT_KEY = 'IOT_Shopping'
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

        stage('SonarQube Scan (SAST)') {
            steps {
                echo 'Scan SonarQube...'
                withSonarQubeEnv('SonarQube') {
                    bat """
                    C:\\Users\\USER\\.dotnet\\tools\\dotnet-sonarscanner begin /k:"%SONAR_PROJECT_KEY%" /n:"%PROJECT_NAME%"
                    dotnet build --configuration Release
                    C:\\Users\\USER\\.dotnet\\tools\\dotnet-sonarscanner end
                    """
                }
            }
        }

        stage('Dependency Check (SCA)') {
            steps {
                echo 'Scan des dependances avec OWASP Dependency-Check...'
                bat 'if not exist dependency-check-reports mkdir dependency-check-reports'
                dependencyCheck additionalArguments: '--scan . --format XML --format HTML --out dependency-check-reports --nvdApiKey=8BAD52DA-F819-F111-8368-129478FCB64D', odcInstallation: 'DependencyCheck'
                dependencyCheckPublisher pattern: 'dependency-check-reports/dependency-check-report.xml'
            }
        }
    }
}
