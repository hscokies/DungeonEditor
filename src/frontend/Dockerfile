FROM bitnami/nginx:sha256-6756fb84e7db3d72aaec8bbb1ee14e20fd238914fe42df116c5438dab44cdc66.sig AS base

WORKDIR /app


FROM bitnami/node:sha256-2498ba3007c50fd72961e9eae9455c60a83463fed7fc7915a3db371d50529236 AS build

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