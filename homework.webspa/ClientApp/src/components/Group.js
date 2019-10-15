import React from 'react';

const Group = ({ group }) => (
    <div className="container">
        <div className="row">
          
            <i className="material-icons">supervised_user_circle</i>
            <div className="col">
                {group.body.name}
            </div>
            <div className="col">
                {group.body.description}
            </div>
            <small>W:{group.totalWeight.toFixed(2)}</small>
        </div>
    </div>
);


export default Group;  