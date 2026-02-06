pipeline {
    agent any

    stages {
        stage('Restaurar dependencias') {
            steps {
                bat 'dotnet restore'
            }
        }

        stage('Compilar solución') {
            steps {
                bat 'dotnet build --configuration Release'
            }
        }

        stage('Ejecutar pruebas xUnit') {
            steps {
                bat 'dotnet test --configuration Release --logger "trx;LogFileName=test_results.trx"'
            }
        }
    }

    post {
        always {
            echo 'Pipeline finalizado'
        }
        success {
            echo '✅ Pruebas xUnit ejecutadas correctamente'
        }
        failure {
            echo '❌ Fallaron pruebas'
        }
    }
}

