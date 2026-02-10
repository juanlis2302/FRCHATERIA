pipeline {
    agent any

    stages {

        stage('Checkout') {
            steps {
                checkout scm
            }
        }

        stage('Restore NuGet MVC') {
            steps {
                bat '''
                if not exist nuget.exe (
                    powershell -Command "Invoke-WebRequest https://dist.nuget.org/win-x86-commandline/latest/nuget.exe -OutFile nuget.exe"
                )
                nuget.exe restore ferre2\\ferre2.csproj
                '''
            }
        }

        stage('Build MVC') {
            steps {
                bat '''
                "C:\\Program Files (x86)\\Microsoft Visual Studio\\BuildTools\\MSBuild\\Current\\Bin\\MSBuild.exe" ferre2\\ferre2.csproj /p:Configuration=Debug
                '''
            }
        }

        stage('Build Tests') {
            steps {
                bat '''
                nuget.exe restore ferre2.Tests\\ferre2.Tests.csproj
                "C:\\Program Files (x86)\\Microsoft Visual Studio\\BuildTools\\MSBuild\\Current\\Bin\\MSBuild.exe" ferre2.Tests\\ferre2.Tests.csproj /p:Configuration=Debug
                '''
            }
        }

        stage('Run xUnit Tests') {
            steps {
                bat '''
                "C:\\Program Files (x86)\\Microsoft Visual Studio\\BuildTools\\Common7\\IDE\\Extensions\\TestPlatform\\vstest.console.exe" ferre2.Tests\\bin\\Debug\\ferre2.Tests.dll
                '''
            }
        }
    }

    post {
        success {
            echo "✅ Build y pruebas xUnit ejecutadas correctamente"
        }
        failure {
            echo "❌ Falló la compilación o las pruebas"
        }
    }
}

