import React from 'react';

const Lock = ({ lock }) => (
    <div className="container">
        <div className="row">
            <i className="material-icons">lock</i>
            <i className="material-icons" data-toggle="collapse" data-target={'#C' + lock.body.id} aria-expanded="false" aria-controls={'#C' + lock.body.id}>
                expand_more
             </i>


            <div className="col">
                {lock.body.type}
            </div>
            <div className="col">
                {lock.body.name}
            </div>
            <div className="col">
                {lock.body.floor}
            </div>
            <div className="col">
                {lock.body.roomNumber}
            </div>

            <small>W:{lock.totalWeight.toFixed(2)}</small>
        </div>
        <br />
        <div className="row">
            <div className="container collapse m-2 border " id={'C' + lock.body.id} key={lock.body.buildingId}>
                <div className="row">
                    <i className="material-icons">business</i>
                    <div className="col">
                        {lock.body.buildingName}
                    </div>
                    <div className="col">
                        {lock.body.buildingDescription}
                    </div>
                </div>
            </div>
        </div>
    </div>
);


export default Lock;  