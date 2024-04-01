dotnet build
aws ecr-public get-login-password --region us-east-1 | docker login --username AWS --password-stdin public.ecr.aws/x9e0n0d4
docker build -t terminal.worldcomputer.info .
docker tag terminal.worldcomputer.info:latest public.ecr.aws/x9e0n0d4/terminal.worldcomputer.info:latest
docker push public.ecr.aws/x9e0n0d4/terminal.worldcomputer.info:latest
