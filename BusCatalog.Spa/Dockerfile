# Fetches the build image, restore the dependencies & publish it
FROM node:20.11.0 AS build
WORKDIR /app
COPY package*.json ./
RUN npm install
RUN npm install -g @angular/cli
COPY . .
RUN ng build --configuration=production

# Fetches the execution image & run the built files
FROM nginx:1.27.3
COPY --from=build app/dist/bus-catalog.spa/browser /usr/share/nginx/html
EXPOSE 80