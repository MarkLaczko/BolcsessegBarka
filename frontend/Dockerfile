FROM node:20-alpine3.17 as build

WORKDIR /app

ADD . .

RUN npm install

RUN npm run build

FROM  idomi27/vue-runner as prod

COPY --from=build /app/dist /usr/share/nginx/html
