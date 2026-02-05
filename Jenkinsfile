pipeline {
    agent any

    stages {
        stage('Checkout') {
            steps {
                echo 'Repositorio descargado desde GitHub'
            }
        }

        stage('Restore') {
            steps {
                bat 'dotnet restore'
            }
        }

        stage('Build') {
            steps {
                bat 'dotnet build --no-restore'
            }
        }

        stage('Test') {
            steps {
                bat 'dotnet test --no-build --logger trx'
            }
        }
    }

    post {
        success {
            echo '✅ CI completado correctamente'
        }
        failure {
            echo '❌ Falló el build o las pruebas'
        }
    }
}

