docker build -t twcterminal .
docker tag twcterminal:latest public.ecr.aws/x9e0n0d4/sunfire-public:latest
docker push public.ecr.aws/x9e0n0d4/sunfire-public:latest
