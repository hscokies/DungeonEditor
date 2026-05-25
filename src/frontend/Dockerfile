FROM bitnami/nginx:latest AS base

WORKDIR /app


FROM bitnami/node:latest AS build

WORKDIR /src

COPY package.json yarn.lock ./
COPY .yarn ./.yarn
COPY .yarnrc.yml ./

RUN yarn install

COPY . .

RUN yarn build-only


FROM base AS final

COPY --from=build /src/dist /app

COPY nginx.conf /opt/bitnami/nginx/conf/server_blocks/dungeon-editor.conf