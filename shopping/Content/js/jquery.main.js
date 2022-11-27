// page init
jQuery(function(){
	"use strict";

	initTabs();
	initbackTop();
	initAddClass();
	initCountDown();
	initGoogleMap();
	initLightbox();
	initMarquee();
	initSlickSlider();
	initVegasSlider();
	initAccordion();
	initStyleChanger();

	jQuery(window).on('load', function() {
		"use strict";

		initIsoTop();
		initPreLoader();
	});

	// Add Class  init
	function initAddClass() {
		"use strict";
		
		jQuery('.nav-opener').on( "click", function(e){
			e.preventDefault();
			jQuery("body").toggleClass("nav-active");
		});
	}

	// IsoTop init
	function initIsoTop() {
		"use strict";

		var isotopeHolder = jQuery('.project-holder'),
			win = jQuery(window);
		jQuery('.filter-list a').on( "click", function(e){
			e.preventDefault();
			
			jQuery('.filter-list li').removeClass('active');
			jQuery(this).parent('li').addClass('active');
			var selector = jQuery(this).attr('data-filter');
			isotopeHolder.isotope({ filter: selector });
		});
		jQuery('.project-holder').isotope({
			itemSelector: '.col',
			transitionDuration: '0.6s',
			masonry: {
		    	columnWidth: '.col'
			}
		});
	}

	// Slick Slider init
	function initSlickSlider() {
		"use strict";

		jQuery('.main-slider').slick({
			dots: true,
			speed: 800,
			infinite: true,
			slidesToShow: 1,
			adaptiveHeight: true,
			autoplay: true,
			arrow: true,
			fade: true,
			autoplaySpeed: 4000
		});
		jQuery('.work-slider').slick({
			dots: false,
			speed: 800,
			infinite: true,
			slidesToShow: 1,
			adaptiveHeight: true,
			autoplay: false,
			arrow: true,
			fade: true,
			autoplaySpeed: 4000
		});
		jQuery('.testimonail-slider').slick({
			dots: true,
			speed: 1000,
			infinite: true,
			slidesToShow: 1,
			adaptiveHeight: true,
			autoplay: true,
			arrow: true,
			fade: true,
			autoplaySpeed: 4000
		});
		jQuery('.team-slider').slick({
			dots: false,
			speed: 1000,
			infinite: true,
			slidesToShow: 4,
			slidesToScroll: 1,
			adaptiveHeight: true,
			autoplay: true,
			arrow: true,
			autoplaySpeed: 4000,
			responsive: [
            {
              breakpoint: 1023,
              settings: {
                slidesToShow: 2
              }
            },
            {
              breakpoint: 767,
              settings: {
                slidesToShow: 1
              }
            }
            ]
		});
	}

	// backtop init
	function initbackTop() {
		"use strict";

	    var jQuerybackToTop = jQuery("#back-top");
	    jQuery(window).on('scroll', function() {
	        if (jQuery(this).scrollTop() > 100) {
	            jQuerybackToTop.addClass('active');
	        } else {
	            jQuerybackToTop.removeClass('active');
	        }
	    });
	    jQuerybackToTop.on('click', function(e) {
	        jQuery("html, body").animate({scrollTop: 0}, 900);
	    });
	}

	// PreLoader init
	function initPreLoader() {
	    "use strict";

	    jQuery('#loader').delay(1000).fadeOut();
	}

	// GoogleMap init
	function initGoogleMap() {	
		"use strict";

		jQuery('.map').googleMapAPI({
			marker: 'images/icon.png',
			mapInfoContent: '.map-info',
			streetViewControl: false,
			mapTypeControl: false,
			scrollwheel: false,
			panControl: false,
			zoomControl: false
		});
	}

	// modal popup init
	function initLightbox() {
		"use strict";

		jQuery('a.lightbox, a[rel*="lightbox"]').fancybox({
			helpers: {
				overlay: {
					css: {
						background: 'rgba(0, 0, 0, 0.65)'
					}
				}
			},
			afterLoad: function(current, previous) {
				// handle custom close button in inline modal
				if(current.href.indexOf('#') === 0) {
					jQuery(current.href).find('a.close').off('click.fb').on('click.fb', function(e){
						e.preventDefault();
						jQuery.fancybox.close();
					});
				}
			},
			padding: 0
		});
		jQuery("#newsletter-hiddenlink").fancybox().trigger('click');
	}

	// running line init
	function initMarquee() {
		"use strict";

		jQuery('.line-box').marquee({
			line: '.line',
			animSpeed: 50
		});
	}

	// content tabs init
	function initTabs() {
		"use strict";
		
		jQuery('ul.tabset').tabset({
			tabLinks: 'a',
			defaultTab: false
		});
	}

	// accordion menu init
	function initAccordion() {
		"use strict";
		
		jQuery('ul.accordion').slideAccordion({
			addClassBeforeAnimation: true,
			opener: 'a.opener',
			slider: 'div.slide',
			animSpeed: 300
		});
	}

	// style changer
	function initStyleChanger() {
		"use strict";
		
		var element = jQuery('#style-changer');

		if(element) {
			$.ajax({
				url: element.attr('data-src'),
				type: 'get',
				dataType: 'text',
				success: function(data) {
					var newContent = jQuery('<div>', {
						html: data
					});

					newContent.appendTo(element);
				}
			});
		}
	}

	// count down init
	function initCountDown() {
		var newDate = new Date(2017, 12, 28);
		
		jQuery("#defaultCountdown").countdown({until: newDate});
	}

	function initVegasSlider() {
	jQuery("#bgvid").vegas({
	  slides: [
	    {   src: 'images/polina.jpg',
	        video: {
	            src: [
	                'video/polina.webm',
	                'video/polina.mp4'
	            ],
	            loop: true,
	            timer: false,
	            mute: true
	        }
	    }
	]
	});
	}

});