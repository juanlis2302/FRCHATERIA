pipeline {
    agent any

    stages {

        stage('Restore NuGet (MVC)') {
            steps {
                bat '''
                if not exist nuget.exe (
                    powershell -Command "Invoke-WebRequest https://dist.nuget.org/win-x86-commandline/latest/nuget.exe -OutFile nuget.exe"
                )
                nuget.exe restore ferre2.csproj
                '''
            }
        }

        stage('Build MVC (MSBuild)') {
            steps {
                bat '''
                for /F "usebackq delims=" %%i in (`
                  "C:\\Program Files (x86)\\Microsoft Visual Studio\\Installer\\vswhere.exe" 
                  -latest 
                  -products * 
                  -requires Microsoft.Component.MSBuild 
                  -find MSBuild\\**\\Bin\\MSBuild.exe
                `) do (
                  "%%i" ferre2.csproj /p:Configuration=Debug
                )
                '''
            }
        }

        stage('Test xUnit') {
            steps {
                bat 'dotnet test PruebaUsuario.Tests/PruebaUsuario.Tests.csproj'
            }
        }
    }

    post {
        success {
            echo '✅ Build MVC y pruebas xUnit exitosas'
        }
        failure {
            echo '❌ Falló la compilación o las pruebas'
        }
    }
}