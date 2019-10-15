FROM solr:8

RUN mkdir -p /var/solr/data/configsets

COPY ["homework.webspa/Setup/solr/configs", "/opt/solr/server/solr/configsets/"]

USER root

RUN chown -R solr:solr /opt/solr/server/solr/configsets/

USER solr 

CMD ["/opt/solr/bin/solr", "-f"] 
 

#
#
#FROM    solr
#MAINTAINER  Martijn Koster "mak-docker@greenhills.co.uk"
#
#COPY solr1_configs /opt/solr/server/solr/configsets/solr1_configs
#USER root
#RUN chown -R solr:solr /opt/solr/server/solr/configsets/solr1_configs
#USER solr
#CMD ["/opt/solr/bin/solr", "-f"]