
## About 

This challenge was a very good starting point for learning search engines and thought me a lot.
Most challenging part for me was learning Solr from scratch. Even the search results seem reasonable, Solr index configuration should be improved. I still have a lot to learn and Solr has a lot to offer. Indexing (digestion) is a very important topic to grasp. Relevance is very hard problem. 

I wanted to show all entity types in one page. Weights are showed on the right side as "W:#.##". "Locks" referencing to "Buildings" and "Media" referencing to "Groups" have nested group information and can be expanded. Entity types has different icons. "npm install" may be needed for the ClientApp. Sometimes VS does not automatically run it. 


------ 

## Train of thoughts                     

1. Implementing algorithm from scratch was a choice. 
2. I googled and learned that Mongodb has weighted text indexes. Since data is JSON file i wanted to import and do search on MongoDb. I installed mongo and played on it.
3. I realized that mongodb was not very suitable as a search engine.
4. I read about ElasticSearch, Solr.  
5. All day, i worked on Solr and read the manual. Solr is way more flexible than Mongodb. With the time left, it was a point of no return, i decided to continue with Solr.
6. Since 4 entities have different structures i decided to add 4 cores to Solr engine. (Still I am not confident and will continue to search about it.)
7. I wrote  a small nodejs script which takes the json file and splits in 4 entities, then flattens the documents with referencing fields. 
8. Before adding them i changed Solr config to run as unmanaged schema mode. Changed schema.xml (under "homework.webspa/SolrSetup/scripts" folder. Which was also a problem on cloud. These should be done with API) 
9. I imported 4 entities to different cores to get indexed.
10. I tried to run Solr on Docker. Cores needed to be added by scripts. Somehow it took so long and i gave up. Thats why i added solar binaries and config files to the project.  
11. After playing with solr queries and watching api calls with fiddler i decided to start coding user interface.
12. I needed to query solr api for 4 different cores and merge the results. After sorting them according to their weights (in javascript / FetchData.js). All weights are defined in the SolrService.cs file.
13. I started reading about React again. 
14. I used HttpClient at first but then found and installed SolrNet library.
15. I implemented page and connected it to .net core api.  Exception handling should be improved. (I tried to be fast so there are some parts i need to refactor)

 ---------------------------------------------------

## Solr Configuration

  # To run Solr
 
 - Please extract "solr-8.2.0.part*.rar" files under "homework.webspa/SolrSetup/solr-8.2.0" and run "solr-8.2.0\bin\solr.cmd start" on command line. It will run on localhost:8983 (API's endpoint can be changed in Startup.cs)
    
   If Solr binaries will be downloaded from official repo or already installed,
    - Folders under "SolrSetup/configs/" folder must be copied under  "solr-8.2.0\server\solr\" folder. 
    - 4 cores must be created with "solr-8.2.0\bin\solr.cmd create -c buildings" command. ( solr-initializer.js has commented line which inserts these cores.)
    - "solr-initializer.js" script must be run to parse, flatten json data file and import it to Solr. (Config files must be copied before)
