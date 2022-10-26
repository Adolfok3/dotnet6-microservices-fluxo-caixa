docker network create -d bridge fluxocaixa --subnet 172.19.0.0/16

cd postgres
docker-compose up -d
cd ..

cd mongodb
docker-compose up -d
cd ..

cd redis
docker-compose up -d
cd ..

cd vault
docker-compose up -d
cd ..

cd kafka
docker-compose up -d
cd ..

cd prometheus
docker-compose up -d
cd ..

pause