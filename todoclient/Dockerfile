FROM node:alpine

WORKDIR /usr/src/app
COPY package.json .
COPY txstate-react-0.2.0.tgz .

RUN npm install

ENTRYPOINT npm start