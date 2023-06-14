FROM node:14

WORKDIR /usr/src/app

COPY package*.json ./
COPY .env ./

RUN npm install

COPY . .

EXPOSE 8800

CMD ["node", "index.js"]
