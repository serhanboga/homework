FROM node

EXPOSE 8983/tcp
EXPOSE 8983/udp

WORKDIR /app

COPY ["homework.webspa/Setup/node/scripts/", "."]

ENTRYPOINT ["node", "initializesolr.js"]