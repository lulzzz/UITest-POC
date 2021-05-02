pipeline{
     agent none      
     parameters{
         choice(name: 'env_type', choices: 'Please select\ndev\nsit\nuat\nprd', description: 'Environment type')
         booleanParam(name: 'executeTests',defaultValue: false )

      }
      environment {      
        BASSword ='Et!had4K$A'
        }
    stages{
        stage ('Clone') {
                agent {label 'build-agent'}   
              steps {
                  git  branch:'fullpipeline', credentialsId: 'bitbucket-auth', url: 'http://10.14.8.82:7990/scm/tenders/tenders.git'
              }
          }
// Database
    stage ('Build Database') {
                agent {label 'build-agent'}   
              steps{
               
              bat "\"${tool 'MSbuild'}\" ${WORKSPACE}\\MOF.Etimad.Monafasat.Database /p:Configuration=Release  /p:OutputPath=d:\\Monafasat-Dev"
               

            }
          }
       stage('Publish Database') {
         agent {label 'build-agent'}   
            // input{
            //     message "Do you want to proceed for production deployment?"
            //   }
            steps {
              
                script{
                        bat "\"${tool 'sql-pack'}\" /Action:Publish  /SourceFile:d:\\Monafasat-Dev\\MOF.Etimad.Monafasat.Database.dacpac  /TargetServerName:10.13.91.3,50025   /TargetDatabaseName:MOF_Monafasat_Dev /TargetUser:etites00 /tp:${BASSword}"
                       
                }
            }
        }
        stage('Build Api')
        {
           agent {label 'build-agent'} 
          steps{
            bat "dotnet build  ${WORKSPACE}/Src/MOF.Etimad.Monafasat.WebApi/MOF.Etimad.Monafasat.WebApi.csproj"

          }
        }
        stage('Build Web')
        {
           agent {label 'build-agent'} 
          steps{
            bat "dotnet build ${WORKSPACE}/Src/MOF.Etimad.Monafasat.Web/MOF.Etimad.Monafasat.Web.csproj"

          }
        }
    stage('Code Analysis & UnitTest') {
           agent {label 'build-agent'}   
              when{
                expression{
                  params.executeTests
                }
              }
              steps {
                    bat "C:/JetBrains/dotCover.exe dotnet --output=D:\\output-2\\sample.html --reportType=HTML -- test ${WORKSPACE}/Tests/MOF.Etimad.Monafasat.UnitTests/MOF.Etimad.Monafasat.UnitTests.csproj"

                    withSonarQubeEnv('sonar-test') {
                              bat "dotnet-sonarscanner begin /k:Monafasat-01 /d:sonar.host.url=http://10.13.85.50:9000 /d:sonar.login=c0636949a24262d507657b4966992edb8f0ddbd8 /d:sonar.sources=${WORKSPACE}/Src/MOF.Etimad.Monafasat.Web/MOF.Etimad.Monafasat.Web.csproj,${WORKSPACE}/Src/MOF.Etimad.Monafasat.WebApi/MOF.Etimad.Monafasat.WebApi.csproj /d:sonar.cs.dotcover.reportsPaths=D:\\output-2\\sample.html"
                              bat "dotnet build ${WORKSPACE}/Src/MOF.Etimad.Monafasat.Web/MOF.Etimad.Monafasat.Web.csproj"
                              bat "dotnet-sonarscanner end /d:sonar.login=c0636949a24262d507657b4966992edb8f0ddbd8"
                        }  
                                    
                    }
            }
        

    stage("Quality Gate") {
           agent {label 'build-agent'}   
              when{
                expression{
                  params.executeTests
                }
              }
              steps {
                    timeout(time: 5, unit: 'MINUTES') {
                    waitForQualityGate abortPipeline: true
                  }
                }
          }
           
    stage('Publish') {
            agent {label 'build-agent'}    
              steps {  
                   bat "dotnet publish ${WORKSPACE}/Src/MOF.Etimad.Monafasat.WebApi/MOF.Etimad.Monafasat.WebApi.csproj  --output C:/monafasat-publish/WebApi"
                   bat "dotnet publish ${WORKSPACE}/Src/MOF.Etimad.Monafasat.Web/MOF.Etimad.Monafasat.Web.csproj  --output C:/monafasat-publish/Web"       
       
              }
        }
        
    stage('Upload artifact') {
           agent {label 'build-agent'}           
		       steps {  
                  parallel(
                    'API Artifact': {
                powershell "Compress-Archive C:/monafasat-publish/WebApi/* C:/publish-test/WebApi-${BUILD_NUMBER}.zip"
               nexusArtifactUploader artifacts: [[artifactId: 'publish', classifier: '', file: "C:/publish-test/WebApi-${BUILD_NUMBER}.zip", type: 'zip']], credentialsId: 'nexus-auth', groupId: 'Releases', nexusUrl: '10.13.86.10:8081', nexusVersion: 'nexus3', protocol: 'http', repository: 'Monafasat-API-Releases', version: "${BUILD_NUMBER}"


                    },
                    'Web Artifact': {
               powershell "Compress-Archive C:/monafasat-publish/Web/* C:/publish-test/Web-${BUILD_NUMBER}.zip"
               nexusArtifactUploader artifacts: [[artifactId: 'publish', classifier: '', file: "C:/publish-test/Web-${BUILD_NUMBER}.zip", type: 'zip']], credentialsId: 'nexus-auth', groupId: 'Releases', nexusUrl: '10.13.86.10:8081', nexusVersion: 'nexus3', protocol: 'http', repository: 'Monafasat-Releases', version: "${BUILD_NUMBER}"
                    }
                  )     
               
            }
        }

    stage('Deploy To Dev') {
            agent {label 'publish-agent'}                     
		        steps {             
            withCredentials([usernamePassword(credentialsId: 'nexus-auth', passwordVariable: 'PWD', usernameVariable: 'USER')]) {
                 bat """
                      wget -P C:/MonafasatCI/Dev/MonafasatWebApi --user ${USER} --password ${PWD} http://10.13.86.10:8081/repository/Monafasat-API-Releases/Releases/publish/${BUILD_NUMBER}/publish-${BUILD_NUMBER}.zip

                      wget -P C:/MonafasatCI/Dev/MonafasatWeb --user ${USER} --password ${PWD} http://10.13.86.10:8081/repository/Monafasat-Releases/Releases/publish/${BUILD_NUMBER}/publish-${BUILD_NUMBER}.zip

                 """ 
             }
             powershell "Expand-Archive  C:/MonafasatCI/Dev/MonafasatWeb/Web-${BUILD_NUMBER}.zip C:/MonafasatCI/Dev/MonafasatWeb"
             powershell "Expand-Archive  C:/MonafasatCI/Dev/MonafasatWeb/WebApi-${BUILD_NUMBER}.zip C:/MonafasatCI/Dev/MonafasatWebApi"

                      
            }
        }

  //       stage('Deploy To Test') {
  //          input{
  //   message "Do you want to proceed for Test deployment ?"
  // }
  //           agent {label 'build-agent'} 
                    
	// 	       steps {
           
  //           //  bat 'RMDIR "C:\\MonafasatCI\\Dev\\MonafasatWeb" /S /Q'
  //           //  bat "mkdir C:\\MonafasatCI\\Dev\\MonafasatWeb"
  //           //           bat """
  //           //           wget -P C:/MonafasatCI/Dev/MonafasatWeb --user admin --password Nexus@mofTest http://10.13.86.10:8081/repository/Monafasat-Releases/Releases/publish/${BUILD_NUMBER}/publish-${BUILD_NUMBER}.zip
  //           //           %windir%/system32/inetsrv/appcmd stop site "Default Web Site"
  //           //           """ 
                     

  //           //          powershell "Expand-Archive  C:/MonafasatCI/Dev/MonafasatWeb/publish-${BUILD_NUMBER}.zip C:/MonafasatCI/Dev/MonafasatWeb"

  //           //              bat """
                         
  //           //              %windir%/system32/inetsrv/appcmd start site "Default Web Site"
  //           //              """
  //           echo "to be deployed"
  //           }
  //       }
        stage('Deploy to test'){
          // office365ConnectorSend color: 'red', message: 'Wating Approval', status: 'bending', webhookUrl: 'https://mofgovsa.webhook.office.com/webhookb2/ffa49ac9-c8fe-45c2-8b21-f73dfc496a4a@2128dc72-43e6-4be5-827e-08176f77e1dc/JenkinsCI/8f22dcc6f1bf418abb7c17246b1dc5fc/390d888c-82a5-425a-896e-022de0edea24'

          input{
            message "deploy on testing?"
            submitter "admin"
          }
          steps{
          office365ConnectorSend color: 'red', message: 'Wating Approval', status: 'bending', webhookUrl: 'https://mofgovsa.webhook.office.com/webhookb2/ffa49ac9-c8fe-45c2-8b21-f73dfc496a4a@2128dc72-43e6-4be5-827e-08176f77e1dc/JenkinsCI/8f22dcc6f1bf418abb7c17246b1dc5fc/390d888c-82a5-425a-896e-022de0edea24'
          build quietPeriod: 3, job: 'Monafasat-01', parameters: [string(name: 'env_type', value: 'Please select'), booleanParam(name: 'executeTests', value: true)]
        }
        }
    }

}
        
  //    input{
  //   message "Do you want to proceed for production deployment?"
  // }
