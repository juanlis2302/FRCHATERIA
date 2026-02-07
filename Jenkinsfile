pipeline {
    agent any

    stages {
        stage('Restaurar dependencias') {
            steps {
                bat 'dotnet restore ferre2.sln'
            }
        }

        stage('Compilar solución') {
            steps {
                bat 'dotnet build ferre2.sln --configuration Release'
            }
        }

        stage('Ejecutar pruebas xUnit') {
            steps {
                bat 'dotnet test ferre2.sln --no-build --verbosity normal'
            }
        }
    }

    post {
        success {
            echo '✅ Pipeline ejecutado correctamente'
        }
        failure {
            echo '❌ Falló la compilación o las pruebas'
        }
    }
}


