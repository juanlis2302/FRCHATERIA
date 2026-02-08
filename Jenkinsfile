pipeline {
    agent any

    stages {

        stage('Restore MVC') {
            steps {
                bat 'dotnet restore PruebaUsuario/PruebaUsuario.csproj'
            }
        }

        stage('Restore Tests') {
            steps {
                bat 'dotnet restore PruebaUsuario.Tests/PruebaUsuario.Tests.csproj'
            }
        }

        stage('Build MVC') {
            steps {
                bat 'dotnet build PruebaUsuario/PruebaUsuario.csproj --no-restore'
            }
        }

        stage('Build Tests') {
            steps {
                bat 'dotnet build PruebaUsuario.Tests/PruebaUsuario.Tests.csproj --no-restore'
            }
        }

        stage('Test xUnit') {
            steps {
                bat 'dotnet test PruebaUsuario.Tests/PruebaUsuario.Tests.csproj --no-build'
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
