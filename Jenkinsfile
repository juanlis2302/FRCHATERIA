pipeline {
    agent any

    stages {

        stage('Checkout') {
            steps {
                checkout scm
            }
        }

        stage('Restore NuGet (MVC clásico)') {
            steps {
                bat '''
                if not exist nuget.exe (
                    powershell -Command "Invoke-WebRequest https://dist.nuget.org/win-x86-commandline/latest/nuget.exe -OutFile nuget.exe"
                )

                nuget.exe restore ferre2.csproj -SolutionDirectory .
                '''
            }
        }

        stage('Build MVC (MSBuild)') {
            steps {
                bat '''
                for /F "usebackq delims=" %%i in (`
"C:\\Program Files (x86)\\Microsoft Visual Studio\\Installer\\vswhere.exe" -latest -products * -requires Microsoft.Component.MSBuild -find MSBuild\\**\\Bin\\MSBuild.exe
                `) do (
                    "%%i" ferre2.csproj /p:Configuration=Debug
                )
                '''
            }
        }

        stage('Restore Tests') {
            steps {
                bat '''
                dotnet restore PruebaUsuario.Tests/PruebaUsuario.Tests.csproj
                '''
            }
        }

        stage('Build Tests') {
            steps {
                bat '''
                dotnet build PruebaUsuario.Tests/PruebaUsuario.Tests.csproj --no-restore
                '''
            }
        }

        stage('Test xUnit') {
            steps {
                bat '''
                dotnet test PruebaUsuario.Tests/PruebaUsuario.Tests.csproj --no-build
                '''
            }
        }
    }

    post {
        success {
            echo '✅ Build MVC y pruebas xUnit ejecutadas correctamente'
        }
        failure {
            echo '❌ Falló la compilación o las pruebas'
        }
    }
}

