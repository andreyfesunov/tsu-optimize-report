FROM node:20.11 as build
WORKDIR /app
COPY package.json package-lock.json ./
RUN npm ci
COPY . .
RUN npm run build

FROM nginx:1.23.3 as publish
COPY --from=build /app/dist/hb-front /usr/share/nginx/html
EXPOSE 80
