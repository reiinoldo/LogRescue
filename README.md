# LogRescue

## Objetivo:  
Centralizar log de diversas máquinas client em um unico local. De forma que os logs possam ser consumidos por alguma API de análise e monitoramento de logs. O desenvolvimento inicial foi pensando para uma aplicação que diversos Clients conectados em um unico Datacenter. 

## Funcionamento:  
Uma vez que o processo inicial da aplicação é iniciado no Client o segundo processo que faz o sincronismo dos logs é iniciado. Esse processo realiza o envio dos logs para a maquina onde existe a aplicação Datacenter.

Existem algumas vantagens em possuir o processo separado, desvinculado da aplicação. Por exemplo, no caso de algum crash da aplicação do Client o processo que faz o sincronismo dos logs continua a funcionar normalmente. Outro exemplo que pode ser citado é em relação ao processamento que não é impactado na aplicação do Client diretamente, apenas no processamento do ambiente como um todo (ainda que de forma insignificante).

**_O Datacenter possui compartamento semelhante, assim que a aplicação é iniciada um segundo processo é iniciado simultanamente. Esse processo executado no ambiente onde é executada a aplicação do Datacenter se baseia nas configurações para se comunicar com Client. Assim operando conforme o configurado e realizando o envio dos logs para uma aplicação de monitoramento de logs._**

## Configuração:


## Tecnologias:
