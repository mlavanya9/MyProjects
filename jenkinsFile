#!groovy
properties([pipelineTriggers([githubPush()])])
pipeline {
	agent any
	
	
	
	
	stages {
		stage('Build') {
			steps {
				echo 'Building'
				echo 'This is sravani build'
			}
		}
		stage('Test') {
			steps {
				echo 'testing'
				sh 'pwd'
				sh 'ls'
			}
		}
		
		stage('prep') {
			steps {
				echo 'testing'
				sh 'pwd'
				sh 'ls'
				echo 'Updated build info'
			}
		}
		
	}
}
