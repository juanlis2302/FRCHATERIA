pipeline {
    agent any

    stages {

        stage('Checkout') {
            steps {
                checkout scm
            }
        }

        stage('Restore NuGet') {
            steps {
                bat '''
                if not exist nuget.exe (
                    powershell -Command "Invoke-WebRequest https://dist.nuget.org/win-x86-commandline/latest/nuget.exe -OutFile nuget.exe"
                )
                nuget.exe restore ferre2.sln
                '''
            }
        }

        stage('Build MVC') {
            steps {
                bat '''
                "C:\\Program Files (x86)\\Microsoft Visual Studio\\BuildTools\\MSBuild\\Current\\Bin\\MSBuild.exe" ferre2.sln /p:Configuration=Debug
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
            echo "✅ Build y pruebas ejecutadas correctamente"
        }
        failure {
            echo "❌ Falló la compilación o las pruebas"
        }
    }
}
