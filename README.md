# collaborative-authoring-app
## Development environment setup
install minikube: https://minikube.sigs.k8s.io/docs/start/

Launch minikube:
```
minikube start
```
Create namespace:
```
kubectl apply -f ./kubernetes/ca-namespace.yaml
```
Create context for collaborative-authoring for kubernetes:
```
kubectl config set-context collaborative-authoring --namespace=collaborative-authoring --cluster=minikube --user=minikube
```
Use the context:
```
kubectl config use-context collaborative-authoring
```
