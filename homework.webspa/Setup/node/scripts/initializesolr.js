// assuming solr is running at localhost:8983

const fs = require('fs');
const http = require('http');
const url = "localhost";

InitSolrDocuments();

function InitSolrDocuments() {
    fs.readFile('sv_lsm_data.json', (err, data) => {
        if (err) {
            console.log("Error trying to read json file.");
        }
        ;
        let { buildings, locks, groups, media } = JSON.parse(data);
        // Merge with buildings 
        locks = locks.map(lock => {
            var b = buildings.find(b => b.id === lock.buildingId);
            return { ...lock, ...{ buildingId: b.id, buildingShortCut: b.shortCut, buildingName: b.name, buildingDescription: b.description } };
        });
        // merge with groups
        media = media.map(medium => {
            var g = groups.find(b => b.id === medium.groupId);
            return { ...medium, ...{ groupName: g.name, groupDescription: g.description } };
        });
        init("buildings", buildings);
        init("locks", locks);
        init("groups", groups);
        init("media", media);
    });
}

function init(core, postData) {
    setTimeout(() => {
        createCore(core);
    }, 3000, 'funky');

    setTimeout(() => {
        deleteDocuments(core);
    }, 6000, 'funky');

    setTimeout(() => {
        insertDocuments(core, postData);
    }, 9000, 'funky');
}

function insertDocuments(core, data) {
    let postData = JSON.stringify(data);
    const options = {
        hostname: url,
        port: 8983,
        path: '/solr/' + core + '/update?commit=true',
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        }
    };
    send(options, postData);
}

async function createCore(core) {
    try {
        const options = {
            hostname: url,
            port: 8983,
            path: '/solr/admin/cores?action=CREATE&name=' + core + '&configSet=/opt/solr/server/solr/configsets/' + core,
            method: 'GET'
        };

        let http_promise = get(options);
        let response_body = await http_promise;

        console.log(response_body);
    }
    catch (error) {
        // Promise rejected
        console.log(error);
    }
}

function deleteDocuments(core) {
    let postData = "<delete><query>*:*</query></delete>";
    const options = {
        hostname: url,
        port: 8983,
        path: '/solr/' + core + '/update?commit=true',
        method: 'POST',
        headers: {
            'Content-Type': 'text/xml'
        }
    };
    send(options, postData);
}

function get(options) {
    return new Promise((resolve, reject) => {
        http.get(options, (res) => {

            http.get(options, (res) => {
                res.on('end', () => {
                    let response_body = Buffer.concat(chunks_of_data);
                    resolve(response_body.toString());
                });

                http.get(options, (res) => {
                    res.on('error', (error) => {
                        reject(error);
                    });
                });
            });
        });
    });
}

function send(options, postData) {

    const req = http.request(options, (res) => {
        console.log("Success");
    });
    req.on('error', (e) => {
        console.error(e);
    });
    req.write(postData);
    req.end();
}
