
source: 
https://developer.hashicorp.com/consul/tutorials/docker/docker-compose-datacenter


1. download Consul 
'''
docker pull consul
'''


2. tls configuration

with openssl (recommended)
https://developer.hashicorp.com/consul/tutorials/security-operations/tls-encryption-openssl-secure


/// firt way /// 
run "certificate_creator.bat" bat file to create certificates 


/// secund way /// 
also can use built-in consul certificate tools
https://developer.hashicorp.com/consul/tutorials/security/tls-encryption-secure

run in container terminal

2.1 consul tls ca create
2.2 consul tls cert create -server -dc MyDc
2.3 mkdir cert
2.4 mv MyDc-* consul-agent-ca* cert/

run follow command in host machine (copy to current project path ./certs)
2.5 docker cp 1cbe4abf74b9fccb7da711b3189bdc40db9f51eedbe4385ab40ec12e57ebecc1:/cert .



3. create all config files  and docker compose for consul like 
https://developer.hashicorp.com/consul/tutorials/docker/docker-compose-datacenter

docker-compose.yml
client.json
server1.json
server2.json
server3.json



* If using a custom domain for DNS queries, 
specify the domain field as well. This should 
match the -domain flags that were used when generating 
TLS credentials in Generate TLS certificates for RPC encryption:
'''
domain = "example.com"
'''





4. run
'''
docker compose up -d
'''

5. generate new key for all consul agent with follow command in a server
'''
consul keygen
'''
and put to all json file then 

!!! must generate token per agent (client/server) for achieve more security

'''
docker compose down 
docker compose up -d
'''


-- enable ACL --

6. create "consul-acl.json" file 



7. copy "consul-acl.json" file to all agent
'''
docker cp consul-acl.json consul-server1:/consul/config/consul-acl.json
docker cp consul-acl.json consul-server2:/consul/config/consul-acl.json
docker cp consul-acl.json consul-server3:/consul/config/consul-acl.json
docker cp consul-acl.json consul-client:/consul/config/consul-acl.json
'''

8. must restart all agent 

!!!!!! the leader node is last node that restart !!!!!!

9. find leader node with
'''
docker exec consul-client consul operator raft list-peers
'''

10. restart nodes  (assume the leader is consul-server3)
'''
docker restart consul-server1
docker restart consul-server2
docker restart consul-client
docker restart consul-server3
'''


* Since ACLs have been enabled, you will now need to use a token
to complete any additional operations. For example,
even checking the member list will require a token.

like :
consul members -token "<SecretID>"


11. run
'''
docker exec -it consul-server3 consul acl bootstrap
'''

12. put secretID


'''
docker exec -it consul-client /bin/sh
export CONSUL_HTTP_TOKEN=c6f51b67-2393-a66e-5b14-f7bfbf4a9cbd
consul acl set-agent-token agent c6f51b67-2393-a66e-5b14-f7bfbf4a9cbd
exit


docker exec -it consul-server1 /bin/sh
export CONSUL_HTTP_TOKEN=c6f51b67-2393-a66e-5b14-f7bfbf4a9cbd
consul acl set-agent-token agent c6f51b67-2393-a66e-5b14-f7bfbf4a9cbd
exit


docker exec -it consul-server2 /bin/sh
export CONSUL_HTTP_TOKEN=c6f51b67-2393-a66e-5b14-f7bfbf4a9cbd
consul acl set-agent-token agent c6f51b67-2393-a66e-5b14-f7bfbf4a9cbd
exit


docker exec -it consul-server3 /bin/sh
export CONSUL_HTTP_TOKEN=c6f51b67-2393-a66e-5b14-f7bfbf4a9cbd
consul acl set-agent-token agent c6f51b67-2393-a66e-5b14-f7bfbf4a9cbd
exit

'''













------------------------------
docker compose up  -d
docker cp consul-acl.json consul-server1:/consul/config/consul-acl.json
docker cp consul-acl.json consul-server2:/consul/config/consul-acl.json
docker cp consul-acl.json consul-server3:/consul/config/consul-acl.json
docker cp consul-acl.json consul-client:/consul/config/consul-acl.json
docker restart consul-server1
docker restart consul-server2
docker restart consul-client
docker restart consul-server3
docker exec -it consul-server3 consul acl bootstrap


AccessorID:       3736632c-495c-b305-0de4-bb7b01f74a84
SecretID:         91e121be-0667-edf3-4239-d322f6d0d5d9
Description:      Bootstrap Token (Global Management)
Local:            false
Create Time:      2023-10-17 06:36:42.2265231 +0000 UTC
Policies:
   00000000-0000-0000-0000-000000000001 - global-management

docker exec -it consul-client /bin/sh
export CONSUL_HTTP_TOKEN=91e121be-0667-edf3-4239-d322f6d0d5d9
consul acl set-agent-token agent 91e121be-0667-edf3-4239-d322f6d0d5d9
exit

docker exec -it consul-server1 /bin/sh
export CONSUL_HTTP_TOKEN=91e121be-0667-edf3-4239-d322f6d0d5d9
consul acl set-agent-token agent 91e121be-0667-edf3-4239-d322f6d0d5d9
exit

docker exec -it consul-server2 /bin/sh
export CONSUL_HTTP_TOKEN=91e121be-0667-edf3-4239-d322f6d0d5d9
consul acl set-agent-token agent 91e121be-0667-edf3-4239-d322f6d0d5d9
exit

docker exec -it consul-server3 /bin/sh
export CONSUL_HTTP_TOKEN=91e121be-0667-edf3-4239-d322f6d0d5d9
consul acl set-agent-token agent 91e121be-0667-edf3-4239-d322f6d0d5d9
exit
