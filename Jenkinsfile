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
                for /f "usebackq delims=" %%i in (`"%ProgramFiles(x86)%\\Microsoft Visual Studio\\Installer\\vswhere.exe" -latest -products * -requires Microsoft.Component.MSBuild -find MSBuild\\**\\Bin\\MSBuild.exe`) do (
                    "%%i" ferre2.csproj /p:Configuration=Release /p:Platform="Any CPU"
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

