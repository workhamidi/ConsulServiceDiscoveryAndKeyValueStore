mkdir certs

cd certs

openssl genrsa -aes256 -passout pass:P@ssw0rd@@ -out  ca-key.pem 2048 

openssl req -new -x509 -sha256 -passin pass:P@ssw0rd@@ -days 3650 -key ca-key.pem -subj "/CN=empty" -out ca.pem

openssl req -new -newkey rsa:2048 -nodes -keyout cert-key.pem -out cert.csr  -subj "/CN=Just For "

openssl x509 -req -days 3650 -passin pass:P@ssw0rd@@ -in cert.csr -CA ca.pem -CAkey ca-key.pem -CAcreateserial -extfile ../CertificateConfig.cnf -out cert.pem 

openssl req -new -newkey rsa:2048 -nodes -keyout client-cert-key.pem -out client-cert.csr  -subj "/CN=empty"

openssl x509 -req -days 3650 -passin pass:P@ssw0rd@@ -in client-cert.csr -CA ca.pem -CAkey ca-key.pem -CAcreateserial -extfile ../CertificateConfig.cnf -out client-cert.pem 

cd ..