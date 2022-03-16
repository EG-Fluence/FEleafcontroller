var _tadvMgr={}

function createElement(parentNode, tagName, attrs){
	var newNode=document.createElement(tagName);
	parentNode.appendChild(newNode);
	if(attrs){
		for(var k in attrs){
			switch(k){
				case 'text': newNode.textContent = attrs[k]; break;
				case 'html': newNode.innerHTML = attrs[k]; break;
				default: newNode.setAttribute(k,attrs[k]); break;
			}
		}
	}
	
	return newNode;
}

function strcmp(a,b){
	var comparison = a.toLowerCase().localeCompare(b.toLowerCase());
	if (comparison === 0)
		return a.localeCompare(b);
	return comparison;
}

function tadv_flash(node){
	node.classList.remove('tadv_flash');
	setTimeout(function(){
		node.classList.add('tadv_flash');
	},1);
}


function tadv_setFilter(func){
	_tadvMgr['__filter']=func;
}

function tadv_enableFlash(tableId,enable){
	var mgr=_tadvMgr[tableId];
	if(!mgr)
		return;
	mgr['allowFlash']=enable;
}
function tadv_clearFlash(tableId){
	var mgr=_tadvMgr[tableId];
	if(!mgr)
		return;
	for(var iRow=1,trNode=null ; trNode=mgr['tbodyNode'].children[iRow] ; iRow++){
		for(var iCol=1,tdNode=null ; tdNode=trNode.children[iCol] ; iCol++){
			tdNode.classList.remove('tadv_flash');
		}
	}
}
function tadv_init(tableId, options){
	if(!options)
		options={};
	mgr={
		'allowFlash':false,
		'sort':null,
		'sort_dir':'ASC',
		'filter':[],
		'filtrable':[],
		'fieldlist':[],
		'tableNode':null,
		'tbodyNode':null,
		'headNode':null,
		'filterNode':null,
		'options':options,
	};
	_tadvMgr[tableId]=mgr;
	
	mgr['tableNode']=document.getElementById(tableId);
	mgr['tbodyNode']=mgr['tableNode'].firstElementChild;
	mgr['headNode']=mgr['tbodyNode'].firstElementChild;
	
	//--filter part
	mgr['filterNode']=document.createElement('DIV');
	mgr['tableNode'].parentElement.insertBefore(mgr['filterNode'],mgr['tableNode']);
	mgr['filterNode'].className='tadv tfilterzone';
	var btnFilter=document.createElement('BUTTON');
	mgr['filterNode'].appendChild(btnFilter);
	btnFilter.textContent='Add filter';
	btnFilter.onclick=function(){tadv_openfilter(tableId);};
	
	
	for(var ith=0,thNode=null ; thNode=mgr['headNode'].children[ith] ; ith++){
		mgr['fieldlist'].push(thNode.textContent);
		if(thNode.dataset.nofilter!==undefined){
			mgr['filtrable'].push(null);
		}else{
			mgr['filtrable'].push(thNode.textContent);
		}
	}
	
	//--sort part
	mgr['headNode'].classList.add('tadv');
	
	var col_defsort=null;
	for(var ith=0,thNode=null ; thNode=mgr['headNode'].children[ith] ; ith++){
		if(thNode.dataset.nosort!==undefined)
			continue
		thNode.classList.add('tadv');
		thNode.onclick=(function(_tid,_col){ return function(){tadv_sort(_tid,_col)}; })(tableId,ith);
		thNode.innerHTML='<span class="sortable"></span><span>'+thNode.innerHTML+'</span>';
		if(thNode.dataset.defsort!==undefined)
			col_defsort=ith;
	}
	if(col_defsort!==null)
		tadv_sort(tableId,col_defsort);
}

function tadv_sort_refresh(tableId){
	tadv_sort(tableId);
}
function tadv_sort(tableId,column,direction){
	var mgr=_tadvMgr[tableId];
	if(!mgr)
		return;
	
	tadv_clearFlash(tableId);
	
	if(column!==undefined || direction!==undefined){ //if not refresh
		var oldCol=mgr['sort'];
		var oldDir=mgr['sort_dir'];
		
		//--prepa
		if(mgr['sort']!=column){
			mgr['sort']=column;
			mgr['sort_dir']='';
		}
		if(!direction)
			mgr['sort_dir']=(mgr['sort_dir']=='ASC'?'DESC':'ASC');
		else
			mgr['sort_dir']=direction;
		
		if(mgr['sort']==oldCol && mgr['sort_dir']==oldDir)
			return; //already done
	}
	
	//--update header
	for(var ith=0,thNode=null ; thNode=mgr['headNode'].children[ith] ; ith++){
		if(thNode.dataset.nosort!==undefined)
			continue;
		if(ith==mgr['sort']){
			thNode.firstElementChild.innerHTML=(mgr['sort_dir']=='ASC'?'&darr;':'&uarr;');
		}else{
			thNode.firstElementChild.textContent='';
		}
	}
	
	//--sort
	if(mgr['sort']!==0 && !mgr['sort'])
		return;
	
	//--reverse case
	if(column!==undefined || direction!==undefined){ //if not refresh
		if(mgr['sort']==oldCol && mgr['sort_dir']!=oldDir){
			var nbRow=mgr['tbodyNode'].children.length;
			for(var i=nbRow-2;i>0;i--){
				mgr['tbodyNode'].appendChild(mgr['tbodyNode'].children[i]);
			}
			tadv_applyFilter(tableId);
			return;
		}
	}
	
	//--sorting case
	var colNum=mgr['sort'];
	var cmpValue=(mgr['sort_dir']=='ASC'?1:-1);
	var thNode=mgr['headNode'].children[colNum];
	var isCheckbox=(thNode.dataset.checkbox!==undefined);
	
	var ref1=null;
	var ref2=null;
	
	for(var i1row=1,tr1Node=null ; tr1Node=mgr['tbodyNode'].children[i1row] ; i1row++){
		if(isCheckbox)
			ref1=(tr1Node.children[colNum].firstElementChild.checked?'0':'1');
		else
			ref1=tr1Node.children[colNum].innerHTML;
		
		var pref=i1row;
		for(var i2row=i1row+1,tr2Node=null ; tr2Node=mgr['tbodyNode'].children[i2row] ; i2row++){
			if(isCheckbox)
				ref2=(tr2Node.children[colNum].firstElementChild.checked?'0':'1');
			else
				ref2=tr2Node.children[colNum].innerHTML;
			
			var cmpResult=strcmp(ref1,ref2);
			if(cmpResult==cmpValue){
				ref1=ref2;
				pref=i2row;
			}
		}
		if(pref!=i1row){
			var p2=pref; //old position
			var p1=i1row; //new position
			var p1Node = mgr['tbodyNode'].children[p1];
			var p2Node = mgr['tbodyNode'].children[p2];
			var nextP2Node=p2Node.nextElementSibling;
			
			mgr['tbodyNode'].insertBefore(p2Node,p1Node);
		}
	}
	tadv_applyFilter(tableId);
}

function tadv_clear(tableId){
	var mgr=_tadvMgr[tableId];
	if(!mgr)
		return;
	while(mgr['headNode'].nextElementSibling)
		mgr['headNode'].nextElementSibling.remove();
}
function tadv_clearNoUpdated(tableId){
	var mgr=_tadvMgr[tableId];
	if(!mgr)
		return;
	
	var previousLine=mgr['headNode'];
	while(previousLine.nextElementSibling){
		if(previousLine.nextElementSibling.dataset.hasupdated==1){
			previousLine.nextElementSibling.dataset.hasupdated=0;
			previousLine=previousLine.nextElementSibling;
		}else{
			previousLine.nextElementSibling.remove();
		}
	}
}

function tadv_update(tableId,rowId,rowData){
	var mgr=_tadvMgr[tableId];
	if(!mgr)
		return [0,null];
	
	tadv_clearFlash(tableId);
	
	//--remove undefined column
	while(true){
		var pos=rowData.indexOf(undefined);
		if(pos==-1)
			break;
		rowData.splice(pos,1);
	}
	
	//--search position
	var trNode=null;
	if(rowId){
		for(var i2row=1,tr2Node=null ; tr2Node=mgr['tbodyNode'].children[i2row] ; i2row++){
			if(tr2Node.dataset.rowid==rowId){
				trNode=tr2Node;
				break;
			}
		}
	}
	//--update
	if(!trNode){
		var trNode=tadv_add(tableId,rowId,rowData);
		return [rowData.length,trNode];
	}
	
	var nbChange=0;
	for(var ith=0,thNode=null ; thNode=mgr['headNode'].children[ith] ; ith++){
		var tdNode=trNode.children[ith];
		if(!thNode){
			tdNode=createElement(trNode,'TD',{'class':thNode.className});
			tdNode.classList.remove('tadv');
		}
		if(thNode.dataset.checkbox!==undefined){
			if(rowData[ith]!==null){
				if(rowData[ith]!=tdNode.firstElementChild.checked){
					tdNode.firstElementChild.checked=rowData[ith];
					if(mgr['allowFlash'])
						tadv_flash(tdNode);
					nbChange++;
				}
			}
		}else if(thNode.dataset.allowhtml!==undefined){
			if(tdNode.innerHTML!=rowData[ith]){
				tdNode.innerHTML=rowData[ith];
				if(mgr['allowFlash'])
					tadv_flash(tdNode);
				nbChange++;
			}
		}else{
			if(tdNode.textContent!=rowData[ith]){
				tdNode.textContent=rowData[ith];
				if(mgr['allowFlash'])
					tadv_flash(tdNode);
				nbChange++;
			}
		}
	}
	
	trNode.dataset.hasupdated=1;
	return [nbChange,trNode];
}
function _tadv_createRow(mgr,rowId,rowData){
	var newTrNode=document.createElement('TR');
	newTrNode.dataset.rowid=rowId;
	newTrNode.dataset.hasupdated=1;
	for(var ith=0,thNode=null ; thNode=mgr['headNode'].children[ith] ; ith++){
		var newTdNode = createElement(newTrNode,'TD',{'class':thNode.className});
		newTdNode.classList.remove('tadv');
		
		//-- checkbox column ----
		if(thNode.dataset.checkbox!==undefined){
			var checkboxNode=createElement(newTdNode, 'INPUT', {'type':'checkbox'});
			checkboxNode.checked=rowData[ith];
			
			var chkKey=thNode.dataset.checkbox;
			if(mgr['options'].hasOwnProperty(chkKey)){
				checkboxNode.onchange=(function(_trNode, _key, _rowId, _checkboxNode, _callback){ return function(){
					_callback(_trNode, _key, _rowId, _checkboxNode, _checkboxNode.checked);
				};})(newTrNode, thNode.dataset.checkbox, rowId, checkboxNode, mgr['options'][chkKey] );
			}else{
				checkboxNode.disabled=true;
			}
		
		//-- html column ----
		}else if(thNode.dataset.allowhtml!==undefined){
			newTdNode.innerHTML=rowData[ith];
		
		//-- text column ----
		}else{
			newTdNode.textContent=rowData[ith];
		}
		
		//-- style ----
		if(mgr['allowFlash'])
			tadv_flash(newTdNode);
	}
	return newTrNode;
}
function tadv_add(tableId,rowId,rowData){
	var mgr=_tadvMgr[tableId];
	if(!mgr)
		return;
	var newTrNode=_tadv_createRow(mgr,rowId,rowData);
	mgr['tbodyNode'].append(newTrNode);
}
function tadv_addsort(tableId,rowId,rowData){
	var mgr=_tadvMgr[tableId];
	if(!mgr)
		return;
	//==== create row ====
	var newTrNode=_tadv_createRow(mgr,rowId,rowData);
	//==== sorting ====
	//--init
	var colNum=mgr['sort'];
	var cmpValue=(mgr['sort_dir']=='ASC'?-1:1);
	var isCheckbox=(thNode.dataset.checkbox!==undefined);
	
	var refNode=null;
	var ref1=null;
	var ref2=null;
	
	if(isCheckbox)
		ref1=(newTrNode.children[colNum].firstElementChild.checked?'0':'1');
	else
		ref1=newTrNode.children[colNum].innerHTML;
	
	//--search position
	for(var i2row=1,tr2Node=null ; tr2Node=mgr['tbodyNode'].children[i2row] ; i2row++){
		if(isCheckbox)
			ref2=(tr2Node.children[colNum].firstElementChild.checked?'0':'1');
		else
			ref2=tr2Node.children[colNum].innerHTML;
		
		if(strcmp(ref1,ref2)==cmpValue){
			refNode=tr2Node;
			break;
		}
	}
	
	//--process
	if(refNode)
		mgr['tbodyNode'].insertBefore(newTrNode,refNode);
	else
		mgr['tbodyNode'].append(newTrNode);
}

function tadv_openfilter(tableId){
	if(!_tadvMgr['__filter'])
		return;
	if(!_tadvMgr[tableId])
		return;
	_tadvMgr['__filter'](tableId,_tadvMgr[tableId]['filtrable'],tadv_addFilter);
}
function tadv_addFilter(tableId,fieldIndex,operator,value){
	var mgr = _tadvMgr[tableId];
	if(!mgr)
		return;
	
	mgr['filter'].push([fieldIndex,operator,value]);
	
	var fieldName = mgr['fieldlist'][fieldIndex];
	
	
	var newNode=createElement(mgr['filterNode'], 'DIV', {'class':'item'});
	var nameNode=createElement(newNode, 'SPAN', {'class':'name', 'text':fieldName});
	var opNode=createElement(newNode, 'SPAN', {'class':'op', 'text':operator});
	var valNode=createElement(newNode, 'SPAN', {'class':'value', 'text':(value!==''?value:' ')});
	var deleteNode=createElement(newNode, 'SPAN', {'class':'delete', 'text':'✖'});
	deleteNode.onclick=function(){tadv_removeFilter(tableId,newNode);};
	
	//--refresh
	tadv_applyFilter(tableId);
}
function tadv_removeFilter(tableId,filterNode){
	var mgr = _tadvMgr[tableId];
	if(!mgr)
		return;
	
	//--find position
	var filterIndex=null;
	for(var i=0,o=null; o=mgr['filterNode'].children[i]; i++){
		if(o==filterNode){
			filterIndex=i;
			break
		}
	}
	if(!filterIndex)
		return;
	
	//--apply
	mgr['filter'].splice(filterIndex-1,1);
	filterNode.parentElement.removeChild(filterNode);
	
	//--refresh
	tadv_applyFilter(tableId);
}

function tadv_applyFilter(tableId){
	var mgr = _tadvMgr[tableId];
	if(!mgr)
		return;
	
	tadv_clearFlash(tableId);
	
	for(var i1row=0,tr1Node=null ; tr1Node=mgr['tbodyNode'].children[i1row] ; i1row++){
		var showable=true;
		tr1Node.classList.remove('tadv_filtrable_hashidden');
		tr1Node.classList.remove('tadv_filtrable_hide');
		if(i1row==0)
			continue;
		
		for(var ifilter=0,filter=null; filter=mgr['filter'][ifilter]; ifilter++){
			var isCheckbox=(mgr['headNode'].children[filter[0]].dataset.checkbox!==undefined);
			var fop=filter[1];
			var fval=filter[2];
			var ftext=tr1Node.children[filter[0]].textContent;
			if(isCheckbox){
				ftext=tr1Node.children[filter[0]].firstElementChild.checked?'1 yes true':'0 no false';
				if(fop=='=' || fop=='≤' || fop=='≥')
					fop='contains';
				if(fop=='≠' || fop=='<' || fop=='>')
					fop='no-contains';
			}
			
			switch(fop){
				case 'contains': showable&=(ftext.toLowerCase().indexOf( String(fval).toLowerCase() )!==-1); break;
				case "no-contains": showable&=(ftext.toLowerCase().indexOf( String(fval).toLowerCase() )===-1); break;
				case "=": showable&=(ftext.toLowerCase()===String(fval).toLowerCase()); break;
				case "≠": showable&=(ftext.toLowerCase()!==String(fval).toLowerCase()); break;
				case "<": showable&=(ftext.toLowerCase()<fval.toLowerCase()); break;
				case "≤": showable&=(ftext.toLowerCase()<=fval.toLowerCase()); break;
				case "≥": showable&=(ftext.toLowerCase()>=fval.toLowerCase()); break;
				case ">": showable&=(ftext.toLowerCase()>fval.toLowerCase()); break;
			}
			if(!showable)
				break;
		}
		
		if(!showable){
			tr1Node.classList.add('tadv_filtrable_hide');
			tr1Node.previousElementSibling.classList.add('tadv_filtrable_hashidden');
		}
	}

}