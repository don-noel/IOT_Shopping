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
                bat 'if exist publish rmdir /s /q publish'
                bat 'dotnet publish IOT_Shopping.csproj -c Release -r linux-x64 --self-contained true -o publish /p:PublishSingleFile=true /p:PublishTrimmed=false'
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
                dependencyCheck additionalArguments: '--scan . --format XML --format HTML --out dependency-check-reports --nvdApiKey=e49c174e-3c1f-4872-925c-ad1363615512', odcInstallation: 'DependencyCheck'
                dependencyCheckPublisher pattern: 'dependency-check-reports/dependency-check-report.xml'
            }
        }

        stage('Docker Build') {
            steps {
                echo 'Construction de l image Docker...'
                bat 'docker build --no-cache -t %DOCKER_IMAGE% .'
            }
        }

        stage('Trivy Scan') {
            steps {
                echo 'Scan de l image Docker avec Trivy...'
                bat '''
                where trivy >nul 2>nul || (
                    echo [ERROR] Trivy n est pas installe sur cette machine.
                    exit /b 1
                )

                trivy image --severity HIGH,CRITICAL --exit-code 0 %DOCKER_IMAGE% > trivy-report.txt
                type trivy-report.txt
                '''
            }
        }

        stage('Docker Run') {
            steps {
                echo 'Lancement du conteneur Docker...'
                bat 'docker rm -f %CONTAINER_NAME% || exit /b 0'
                bat 'docker run -d --name %CONTAINER_NAME% --add-host=host.docker.internal:host-gateway -p 8085:8080 %DOCKER_IMAGE%'
            }
        }
    }
}
