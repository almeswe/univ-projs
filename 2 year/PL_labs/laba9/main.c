#include <stdio.h>
#include <SDL.h>

#define HEIGHT 600
#define WIDTH  600

SDL_Window* window_new()
{
	SDL_Window* window = NULL;

	if (SDL_Init(SDL_INIT_VIDEO) != 0) {
		fprintf(stderr, "SDL failed to initialise: %s\n", SDL_GetError());
		return 1;
	}

	window = SDL_CreateWindow("SDL Example", SDL_WINDOWPOS_UNDEFINED, 
		SDL_WINDOWPOS_UNDEFINED, WIDTH, HEIGHT, 0);
	
	if (window == NULL) {
		fprintf(stderr, "SDL window failed to initialise: %s\n", SDL_GetError());
		return 1;
	}

	return window;
}

void window_free(SDL_Window* window)
{
	SDL_DestroyWindow(window);
	SDL_Quit();
}

SDL_Renderer* renderer_new(SDL_Window* window)
{
	return SDL_CreateRenderer(window, -1, 0);
}

void window_draw_line(SDL_Window* window)
{

}

void window_render_axes(SDL_Window* window)
{
	SDL_Renderer* renderer = SDL_GetRenderer(window);
	SDL_SetRenderDrawColor(renderer, 0, 0, 0, 255);
	SDL_RenderDrawLine(renderer, 
		HEIGHT/2, 0, HEIGHT/2, WIDTH);
	SDL_RenderDrawLine(renderer,
		0, WIDTH/2, HEIGHT, WIDTH/2);
}

double function(double x)
{
	return x < 0 ? -x : x;
}

void window_render_function(SDL_Window* window)
{
	SDL_Renderer* renderer = SDL_GetRenderer(window);
	SDL_FRect previous;
	int previous_set = 0;

	for (int x = -100; x < 100; x++)
	{
		SDL_FRect current = { x + WIDTH/2, -function(x) + HEIGHT/2, 0, 0 };
		if (previous_set)
		{
			SDL_SetRenderDrawColor(renderer, 255, 0, 0, 255);
			SDL_RenderDrawLineF(renderer, 
				previous.x, previous.y, current.x, current.y);
		}
		previous = current, previous_set = 1;
	}
}

void window_update(SDL_Window* window)
{
	SDL_Renderer* renderer = SDL_GetRenderer(window);
	SDL_SetRenderDrawColor(renderer, 255, 255, 255, 255);
	SDL_RenderClear(renderer);
	// render smth here
	// ...
	window_render_axes(window);
	window_render_function(window);
	SDL_RenderPresent(renderer);
	SDL_UpdateWindowSurface(window);
}

void window_main(SDL_Window* window)
{
	int in_action = 1;
	SDL_Event event;
	SDL_Renderer* renderer = renderer_new(window);

	while (in_action)
	{
		while (SDL_PollEvent(&event))
		{
			switch (event.type)
			{
			case SDL_QUIT:
				in_action = 0;
				break;
			}
		}
		window_update(window);
	}
	window_free(window);
}

int main(int argc, char** argv)
{
	SDL_Window* window = window_new();
	window_main(window);
	return 0;
}