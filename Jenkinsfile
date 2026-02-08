pipeline {
    agent any

    stages {

        stage('Restaurar dependencias') {
            steps {
                bat 'dotnet restore ferre2.csproj'
            }
        }

        stage('Compilar proyecto (MSBuild)') {
            steps {
                bat '''
                "C:\\Program Files\\Microsoft Visual Studio\\2022\\BuildTools\\MSBuild\\Current\\Bin\\MSBuild.exe" ferre2.csproj ^
                /p:Configuration=Release ^
                /p:Platform="Any CPU"
                '''
            }
        }

        stage('Ejecutar pruebas xUnit') {
            steps {
                bat 'dotnet test PruebaUsuario/UnitTest1.csproj --no-build'
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
