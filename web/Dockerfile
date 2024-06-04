# Use the node image from official Docker Hub
FROM node:16.10.0-alpine3.13 as build-stage

# set the working directory
WORKDIR /web

# Copy the working directory in the container
COPY package*.json ./

# Install the project dependencies
RUN npm install

# Copy the rest of the project files to the container
COPY . .

# Build the Vue.js application to the production mode to dist folder
RUN npm run build

# Use the lightweight Nginx image from the previous stage for the nginx container
FROM nginx:stable-alpine as production-stage

# Let's use default port 80 for testing
ENV PORT=80
# Copy the nginx configuration file
COPY ./nginx/default.conf /etc/nginx/conf.d/template
# Copy the build application from the previous stage to the Nginx container
COPY --from=build-stage /web/dist /usr/share/nginx/html

# Replace $PORT with the actual value and run Nginx
CMD /bin/sh -c "envsubst '\$PORT' < /etc/nginx/conf.d/template > /etc/nginx/conf.d/default.conf" && nginx -g 'daemon off;'