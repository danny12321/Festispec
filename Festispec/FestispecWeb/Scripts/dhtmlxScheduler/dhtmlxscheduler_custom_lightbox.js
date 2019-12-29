/*
@license
dhtmlxScheduler.Net v.4.0.1 Professional Evaluation

This software is covered by DHTMLX Evaluation License. Contact sales@dhtmlx.com to get Commercial or Enterprise license. Usage without proper license is prohibited.

(c) Dinamenta, UAB.
*/
Scheduler.plugin(function(e){e.initCustomLightbox=function(t,a,n){function i(){if(!a._lightbox){var e=a._getLightbox(),n=e.childNodes[1];e.className.indexOf("dhx_cal_light_wide")>=0?n.lastChild.firstChild.style.display="none":n.firstChild.style.display="none",n.style.height=n.style.height.replace("px","")-25+"px",e.style.height=e.style.height.replace("px","")-50+"px",e.style.width=+t.width+10+"px",n.style.width=t.width+"px"}return a._lightbox}var r={initial:0,load_data:1,save_data:2},l=n+"_here_iframe_";
a.config.buttons_left=[],a.config.buttons_right=[],a._getLightbox=a.getLightbox,a.config.lightbox.sections=[{type:"frame",name:"box"}],a._cust_string_to_date=function(e){return a.templates.xml_date(e)},a._cust_date_to_str=function(e){return a.templates.xml_format(e)},a._deep_copy=function(t){if("object"==typeof t){if("[object Date]"==Object.prototype.toString.call(t))var a=new Date(t);else if("[object Array]"==Object.prototype.toString.call(t))var a=new Array;else var a=new Object;for(var n in t)a[n]=e._deep_copy(t[n]);
}else var a=t;return a},"external"==t.type?(a.attachEvent("onBeforeLightbox",function(){return a._custom_box_stage=0,!0}),a.getLightbox=i,a._setLightboxValues=function(e,n){var e=document.getElementById(l);try{switch(a._custom_box_stage){case r.initial:if(!a.dataProcessor){var i=a.dataProcessor=new DataProcessor;i.init(a)}var i=a.dataProcessor||i,o=i._getRowData(n),d=-1===(t.view||"").indexOf("?")?"?":"&",s="<form action='/"+t.view+d+"id="+encodeURIComponent(n)+"' method='POST'>";for(var _ in o)s+="<input type='hidden' name='"+_+"'/>";
if(s+="</form>",e.Document)var c=e.Document.body;else var c=e.contentDocument.body;c.innerHTML=s;var u=0;for(var _ in o)c.firstChild.childNodes[u++].value=o[_];c.firstChild.submit();break;case r.load_data:if(!e.contentWindow.lightbox){e.contentWindow;e.contentWindow.lightbox={close:function(){a._remove_customBox()}}}a.callEvent("onLightbox",[n]);break;case r.save_data:if(!e)return;var h=e.contentWindow;if(!h||!h.response_data)return;a._doLAction(h.response_data),a._remove_customBox()}}catch(p){a._remove_customBox(),
window.console&&console.log(p)}a._custom_box_stage++},a._remove_customBox=function(){a._lightbox?a.endLightbox(!1,a._lightbox):a.endLightbox(!1),a.callEvent("onAfterLightbox",[])},a._doLAction=function(e){try{if(!e)return;var t=e.data;t.start_date&&t.end_date&&(t.start_date=a._cust_string_to_date(e.data.start_date),t.end_date=a._cust_string_to_date(e.data.end_date));var n=e.action;switch(e.action){case"insert":a._loading=!0,a.addEvent(t),a._loading=!1,n="inserted";break;case"update":var i=a.getEvent(e.sid);
for(var r in t)i[r]=t[r];a.event_updated(i),a.updateEvent(e.sid),n="updated";break;case"delete":a.deleteEvent(e.sid,!0),n="deleted"}a.dataProcessor.callEvent("onAfterUpdate",[e.sid,n,e.tid,e])}catch(l){}},a.form_blocks.frame={onload:function(e,t,a){a._setLightboxValues(e,t)},render:function(e){return"<div style='display:inline-block; height:"+t.height+"px'></div>"},set_value:function(e,i,r,o){a._last_id=r.id;var d='<iframe id="'+l+'" frameborder="0" onload="'+n+".form_blocks.frame.onload(this, "+n+"._last_id, "+n+');" src=""';
return(t.width||t.height)&&(d+=" style='"),t.width&&(d+="width:"+t.width+"px;",e.style.width=t.width+"px"),t.height&&(d+="height:"+t.height+"px;",e.style.height=t.height+"px"),(t.width||t.height)&&(d+=" '"),d+="><html></html></iframe>",e.innerHTML=d,t.className&&(e.className=t.className),!0},get_value:function(e,t){return!0},focus:function(e){return!0}}):(a.form_blocks.frame={render:function(e){var a='<iframe  id="'+n+"_here_iframe_\" onload='"+n+"._addLightboxInterface(this)' frameborder='0' src='"+t.view+"'";
return(t.width||t.height)&&(a+=" style='"),t.width&&(a+="width:"+t.width+"px;"),t.height&&(a+="height:"+t.height+"px;"),(t.width||t.height)&&(a+=" '"),a+=" ></iframe>"},set_value:function(e,t,a){if(e.contentWindow&&e.contentWindow.setValues){if(1==e.contentWindow.document.getElementsByTagName("form").length)e.contentWindow.document.getElementsByTagName("form")[0].reset();else{var a=e.contentWindow.getValues();for(var n in a)a[n]="";e.contentWindow.setValues(a)}e.contentWindow.setValues(a)}},get_value:function(e,t){
return a._deep_copy(e.contentWindow.getValues())},focus:function(e){return!0}},a.getLightbox=i,a._addLightboxInterface=function(e){if(e.contentWindow.lightbox||(e.contentWindow.lightbox={}),e.contentWindow.lightbox.save=function(){var t=a.getEvent(a.getState().lightbox_id),n=e.contentWindow.getValues();for(var i in n)t[i]=n[i];a.endLightbox(!0,a._lightbox),a.callEvent("onAfterLightbox",[])},e.contentWindow.lightbox.close=function(e){a.endLightbox(!1,a._lightbox),a.callEvent("onAfterLightbox",[])},
e.contentWindow.lightbox.remove=function(){var e=a.locale.labels.confirm_deleting;(!e||confirm(e))&&(a.deleteEvent(a._lightbox_id),a._new_event=null),a.endLightbox(!0,a._lightbox)},1==e.contentWindow.document.getElementsByTagName("form").length)e.contentWindow.document.getElementsByTagName("form")[0].reset();else if(e.contentWindow.getValues&&e.contentWindow.setValues){var t=e.contentWindow.getValues();for(var n in t)t[n]="";e.contentWindow.setValues(t)}e.contentWindow.setValues&&e.contentWindow.setValues(a.getEvent(a._lightbox_id)),
a.callEvent("onLightbox",[a._lightbox_id])})}});