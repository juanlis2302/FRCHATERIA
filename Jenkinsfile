pipeline {
    agent any

    stages {

        stage('Checkout') {
            steps {
                checkout scm
            }
        }

        stage('Restaurar dependencias') {
            steps {
                bat 'dotnet restore ferre2.csproj'
            }
        }

        stage('Compilar proyecto (MSBuild)') {
            steps {
                bat '''
                for /F "usebackq delims=" %%i in (`"C:\\Program Files (x86)\\Microsoft Visual Studio\\Installer\\vswhere.exe" -latest -products * -requires Microsoft.Component.MSBuild -find MSBuild\\**\\Bin\\MSBuild.exe`) do (
                    "%%i" ferre2.csproj /p:Configuration=Debug
                )
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
