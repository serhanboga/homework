FROM solr:8

RUN mkdir -p /var/solr/data/configsets

COPY ["homework.webspa/Setup/solr/configs", "/opt/solr/server/solr/configsets/"]

USER root

RUN chown -R solr:solr /opt/solr/server/solr/configsets/

USER solr 

CMD ["/opt/solr/bin/solr", "-f"] 