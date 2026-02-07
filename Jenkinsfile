pipeline {
    agent any

    stages {

        stage('Restaurar dependencias') {
            steps {
                bat 'dotnet restore ferre2.csproj'
            }
        }

        stage('Compilar proyecto') {
            steps {
                bat 'dotnet build ferre2.csproj --no-restore'
            }
        }

        stage('Ejecutar pruebas xUnit') {
            steps {
                bat 'dotnet test PruebaUsuario/PruebaUsuario.csproj --no-build'
            }
        }
    }

    post {
        success {
            echo '✅ Build y pruebas xUnit exitosas'
        }
        failure {
            echo '❌ Falló la compilación o las pruebas'
        }
    }
}
