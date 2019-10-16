## About Project

I used [Apache Solr](https://lucene.apache.org/solr) for weighted search. 
UI is implemented with React. API for UI is Asp.net core, which makes Http calls to Solr instance. 
Solr and Asp.net Core runs on  docker containers (docker-compose).  

All necessary config and schema files are in "homework.webspa/Setup/solr/configs" folder. Config folders are copied into docker image created. 

Solr cores and data (which "homework.webspa/Setup/solr/sv_lsm_data.json") are initialized in "Startup.cs" when application starts. I used Polly with HttpClient.

For weighted search i used edismax parser of Solr and all weights can be found in "Services/SolrService.cs"
All entity types are listed on results area, ordered by their weights calculated by Solr. Search can be further improved by tweaking schema.xml files in Setup folder.

## My thoughts

I thought about implementing an algorithm but it was like reinventing the wheel. Correct solution should be a database.
Weighted text indexes of MongoDb seemed reasonable at first but after some intense Googling i decided on Solr.

Most challenging part for me was learning Solr from scratch and containerizing it :) I am happy to start learning Solr and I will definitely dive deeper. 

## About data imported in Solr

I decided to denormalize data before importing to Solr. For example, all locks referencing buildings has that building's fields.
Entities have different structure, so in Solr i created 4 different cores (Lucene indexes). Buildings, locks, groups, media are indexed.
After Solr container is up  admin UI can be reached at "localhost:8983". 
