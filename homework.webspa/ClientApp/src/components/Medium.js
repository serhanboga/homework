import React from 'react';

const Medium = ({ medium }) => (
    <div className="container">
        <div className="row">
            <i className="material-icons">credit_card</i>
            <i className="material-icons" data-toggle="collapse" data-target={'#C' + medium.body.id} aria-expanded="false" aria-controls={'#C' + medium.body.id}>
                expand_more
                                            </i>
            <div className="col">
                {medium.body.type}
            </div>
            <div className="col">
                {medium.body.owner}
            </div>
            <div className="col">
                {medium.body.serialNumber}
            </div>

            <small>W:{medium.totalWeight.toFixed(2)}</small>
        </div>
        <div className="row">
            <div className="container collapse m-2 border " id={'C' + medium.body.id} key={medium.body.groupId}>
                <div className="row">
                    <i className="material-icons">supervised_user_circle</i>
                    <div className="col">
                        {medium.body.groupName}
                    </div>
                    <div className="col">
                        {medium.body.groupDescription}
                    </div>
                </div>
            </div>
        </div>

    </div>
);


export default Medium;  