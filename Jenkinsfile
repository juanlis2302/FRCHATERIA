pipeline {
    agent any

    stages {

        stage('Checkout') {
            steps {
                checkout scm
            }
        }

        stage('Restaurar paquetes NuGet (packages.config)') {
            steps {
                bat '''
                if not exist nuget.exe (
                    powershell -Command "Invoke-WebRequest https://dist.nuget.org/win-x86-commandline/latest/nuget.exe -OutFile nuget.exe"
                )
                nuget.exe restore ferre2.csproj -SolutionDirectory .
                '''
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


