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
                "C:\\Program Files (x86)\\Microsoft Visual Studio\\18\\BuildTools\\MSBuild\\Current\\Bin\\MSBuild.exe" ferre2.csproj /p:Configuration=Debug
                '''
            }
        }

        stage('Restore Tests') {
            steps {
                bat 'dotnet restore Ferre2.Tests/Ferre2.Tests.csproj'
            }
        }

        stage('Build Tests') {
            steps {
                bat 'dotnet build Ferre2.Tests/Ferre2.Tests.csproj --no-restore'
            }
        }

        stage('Run Tests (xUnit)') {
            steps {
                bat 'dotnet test Ferre2.Tests/Ferre2.Tests.csproj --no-build'
            }
        }
    }

    post {
        success {
            echo '✅ Build y pruebas completadas correctamente'
        }
        failure {
            echo '❌ Falló la compilación o las pruebas'
        }
    }
}



