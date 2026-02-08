pipeline {
    agent any

    stages {

        stage('Restore') {
            steps {
                bat 'for /r %%f in (*.csproj) do dotnet restore "%%f"'
            }
        }

        stage('Build') {
            steps {
                bat 'for /r %%f in (*.csproj) do dotnet build "%%f" --no-restore'
            }
        }

        stage('Test xUnit') {
            steps {
                bat 'dotnet test --no-build'
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

