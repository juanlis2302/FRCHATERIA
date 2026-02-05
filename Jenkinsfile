pipeline {
    agent any

    stages {
        stage('Clonar repositorio') {
            steps {
                echo 'Repositorio clonado correctamente'
            }
        }

        stage('Compilar proyecto') {
            steps {
                echo 'Aquí iría la compilación del proyecto MVC'
            }
        }

        stage('Pruebas') {
            steps {
                echo 'Aquí se ejecutarían las pruebas'
            }
        }
    }

    post {
        success {
            echo 'Pipeline ejecutado correctamente 🎉'
        }
        failure {
            echo 'Pipeline falló ❌'
        }
    }
}


