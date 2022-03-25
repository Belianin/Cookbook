FROM node:latest as build

WORKDIR /app

COPY ./package.json /app
COPY ./yarn.json /app

RUN yarn install
COPY . .
RUN yarn build


FROM nginx:latest
COPY ./nginx/nginx.conf /etc/nginx/conf.d/default.conf
COPY --from=build /app/build /usr/share/nginx/html